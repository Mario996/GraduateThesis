<template>
  <v-row align="center"
         justify="center">
    <v-col
      sm="10"
      md="8"
      lg="8">
      <v-card
      color="red lighten-2"
      light>
      <v-card-title class="text-h5 red lighten-3">
        Search for security rules
      </v-card-title>
      <v-card-text>
        <v-autocomplete
          v-model="chosenRequirement"
          :items="searchResults"
          :loading="isLoading"
          :search-input.sync="search"
          color="black"
          hide-no-data
          hide-selected
          item-text="partProse"
          item-value="partId"
          label="Security requirements"
          placeholder="Start typing to Search"
          prepend-icon="mdi-database-search"
          return-object />
      </v-card-text>
      <v-divider />
      <v-expand-transition>
        <v-list
          v-if="chosenRequirement"
          class="red lighten-3">
          <v-list-item
            v-for="(field, i) in fields"
            :key="i">
            <v-list-item-content>
              <v-list-item-title v-text="field.key" />
              <v-list-item-subtitle class="text-wrap"
                                    style="font-weight: bold;"
                                    v-text="field.value" />
            </v-list-item-content>
          </v-list-item>
        </v-list>
      </v-expand-transition>
      <v-card-actions>
        <v-spacer />
        <v-btn
          :disabled="!chosenRequirement"
          color="white darken-3"
          @click="chosenRequirement = null">
          Clear
          <v-icon right>
            mdi-close-circle
          </v-icon>
        </v-btn>
      </v-card-actions>
    </v-card>
      <v-data-table
        :headers="headers"
        :items="priorities"
        class="elevation-1">
        <template #top>
          <v-toolbar flat
                     color="white">
            <v-toolbar-title>Priority of requirements</v-toolbar-title>
            <v-divider
              class="mx-4"
              inset
              vertical />
            <v-spacer />
            <v-dialog v-model="dialog"
                      max-width="500px">
              <template #activator="{ on }">
                <v-btn color="primary"
                       dark
                       class="mb-2"
                       v-on="on">
                  New Priority
                </v-btn>
              </template>
              <v-card>
                <v-card-title>
                  <span class="headline">{{ formTitle }}</span>
                </v-card-title>

                <v-card-text>
                  <v-container>
                    <v-row>
                      <v-col cols="6"
                             sm="6"
                             md="6">
                        <v-text-field v-model="editedPriority.name"
                                      label="Name" />
                      </v-col>
                      <v-col cols="6"
                             sm="6"
                             md="6">
                        <v-text-field v-model="editedPriority.value"
                                      label="Value" />
                      </v-col>
                    </v-row>
                  </v-container>
                </v-card-text>

                <v-card-actions>
                  <v-spacer />
                  <v-btn color="blue darken-1"
                         text
                         @click="close">
                    Cancel
                  </v-btn>
                  <v-btn color="blue darken-1"
                         text
                         @click="save">
                    Save
                  </v-btn>
                </v-card-actions>
              </v-card>
            </v-dialog>
          </v-toolbar>
        </template>
        <template>
          <v-icon
            small
            class="mr-2"
            @click="editPriority(item)">
            mdi-pencil
          </v-icon>
          <v-icon
            small
            @click="deletePriority(item)">
            mdi-delete
          </v-icon>
        </template>
      </v-data-table>
    </v-col>
      <!--v-textarea v-model="search"
                  type="text"
                  @input="searchControls" /-->
  </v-row>
</template>

<script>
import { prioritiesService } from '../services/priorities-service'
import { searchService } from '../services/search-service'

export default {
    data: () => ({
        dialog: false,
        search: '',
        searchResults: [],
        chosenRequirement: '',
        isLoading: false,
        count: 0,
        headers: [
            {
                text: 'Name',
                align: 'start',
                value: 'name',
            },
            { text: 'Value', value: 'value' },
            { text: 'Actions', value: 'actions', sortable: false },
        ],
        priorities: [],
        editedIndex: -1,
        editedPriority: {
            id: '',
            name: '',
            value: 0,
        },
        defaultPriority: {
            id: '',
            name: '',
            value: 0,
        },
    }),
    computed: {
        formTitle () {
            return this.editedIndex === -1 ? 'New Priority' : 'Edit Priority'
        },
        fields () {
            if (!this.chosenRequirement) return []

            return Object.keys(this.chosenRequirement).map(key => {
                if (key === 'suggest') {
                    return {
                        key: null,
                        value: null
                    }
                }
                return {
                    key: this.transformKeyName(key),
                    value: this.chosenRequirement[key] || 'n/a',
                }
            })
        },
    },
    watch: {
        dialog (val) {
            // eslint-disable-next-line no-unused-expressions
            val || this.close()
        },
        search (val) {
            // Items have already been requested
            if (this.isLoading) return

            this.isLoading = true
            searchService.search(this.search).then((response) => {
                this.searchResults = response.map((result) => {
                    return {
                        controlClass: result.controlClass,
                        controlId: result.controlId,
                        controlTitle: result.controlTitle,
                        groupTitle: result.groupTitle,
                        partId: result.partId,
                        partProse: result.partProse
                    }
                })
                this.count = response.count
            }).catch(err => {
                console.log(err)
            })
                .finally(() => (this.isLoading = false))
        },
    },
    created () {
        prioritiesService.getAllPriorities()
            .then((response) => {
                this.priorities = response.map(x => x.source)
            })
    },
    methods: {
        editPriority (item) {
            this.editedIndex = this.priorities.indexOf(item)
            this.editedPriority = Object.assign({}, item)
            this.dialog = true
        },
        deletePriority (item) {
            const index = this.priorities.indexOf(item)
            // eslint-disable-next-line no-unused-expressions
            confirm('Are you sure you want to delete this priority?') && this.priorities.splice(index, 1)
            prioritiesService.deletePriority(item.id)
        },
        close () {
            this.dialog = false
            setTimeout(() => {
                this.editedPriority = Object.assign({}, this.defaultPriority)
                this.editedIndex = -1
            }, 300)
        },
        save () {
            if (this.editedIndex > -1) {
                Object.assign(this.priorities[this.editedIndex], this.editedPriority)
                prioritiesService.updatePriority(this.editedPriority, this.editedPriority.id)
            } else {
                this.priorities.push(this.editedPriority)
                prioritiesService.createPriority(this.editedPriority)
            }
            this.close()
        },
        searchControls () {
            searchService.search(this.search).then((response) => {
                this.searchResults = response.map((result) => {
                    return {
                        controlClass: result.controlClass,
                        controlId: result.controlId,
                        controlTitle: result.controlTitle,
                        groupTitle: result.groupTitle,
                        partId: result.partId,
                        partProse: result.partProse
                    }
                })
                console.log(this.searchResults)
            })
        },
        transformKeyName (key) {
            if (key === 'groupTitle') {
                return 'Group title'
            } else if (key === 'controlId') {
                return 'Control id'
            } else if (key === 'controlClass') {
                return 'Control class'
            } else if (key === 'controlTitle') {
                return 'Control title'
            } else if (key === 'partId') {
                return 'Part id'
            } else if (key === 'partProse') {
                return 'Part prose'
            }
        }
    },
}
</script>
