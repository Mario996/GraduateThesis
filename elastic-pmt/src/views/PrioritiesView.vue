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
            item-text="partProse"
            item-value="partId"
            label="Security requirements"
            placeholder="Start typing to Search"
            prepend-icon="mdi-database-search"
            no-filter
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
          <v-btn color="primary"
                 dark
                 @click="createIndex()">
            Create
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-col>
    <!--v-textarea v-model="search"
                  type="text"
                  @input="searchControls" /-->
  </v-row>
</template>

<script>
import { searchService } from '../services/search-service'
import { indexService } from '../services/index-service'

export default {
    data: () => ({
        search: '',
        searchResults: [],
        chosenRequirement: '',
        isLoading: false,
        count: 0,
    }),
    computed: {
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
        search (val) {
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
                console.log(this.searchResults)
                this.count = response.count
            }).catch(err => {
                console.log(err)
            })
                .finally(() => {
                    this.isLoading = false
                })
        },
    },
    created () {
        indexService.create()
            .then((response) => {
                console.log('CREATED')
                console.log(response)
            })
    },
    methods: {
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
                console.log('SEARCH CONTROLS')
            })
        },
        createIndex () {
            indexService.create().then((response) => {
                console.log('CREATE INDEX')
                console.log(response)
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
