<template>
  <v-row align="center"
         justify="center">
    <v-col
      sm="12"
      md="10"
      lg="10">
      <v-data-table
        :headers="headers"
        :items="requirements"
        :items-per-page="10"
        class="elevation-1">
        <template v-slot:item="{ item }">
          <tr>
            <td>{{ item.controlClass }}</td>
            <td>{{ item.controlId }}</td>
            <td>{{ item.controlTitle }}</td>
            <td>{{ item.groupTitle }}</td>
            <td>{{ item.partId }}</td>
            <td class="truncate">
              <v-tooltip bottom>
                <template v-slot:activator="{ on, attrs }">
                  <span
                    v-bind="attrs"
                    v-on="on">
                    {{ item.partProse }}</span>
                </template>
                <span>{{ item.partProse }}</span>
              </v-tooltip>
            </td>
            <td>
              <v-icon
                medium
                color="red"
                @click="deleteItem(item.id)">
                mdi-delete
              </v-icon>
            </td>
          </tr>
        </template>
      </v-data-table>
    </v-col>
  </v-row>
</template>

<script>
import { requirementService } from '../services/requirement-service'

export default {
    data: () => ({
        requirements: [],
        headers: [
            {
                text: 'Standard',
                align: 'start',
                sortable: false,
                value: 'controlClass',
            },
            { text: 'Control ID', sortable: false, value: 'controlId' },
            { text: 'Control title', sortable: false, value: 'controlTitle' },
            { text: 'Group Title', sortable: false, value: 'groupTitle' },
            { text: 'Part ID', sortable: false, value: 'partId' },
            { text: 'Part prose', sortable: false, value: 'partProse' },
            { text: 'Actions', value: 'actions', sortable: false },
        ]
    }),
    created () {
        requirementService.getAllRequirements().then((response) => {
            this.requirements = response
        })
    },
    methods: {
        deleteItem (requirementId) {
            requirementService.deleteRequirementFromList(requirementId).then((response) => {
                const index = this.requirements.findIndex(x => x.id === requirementId)
                this.requirements.splice(index, 1)
            })
        }
    }
}
</script>

<style scoped>
    .truncate {
       max-width: 20vw;
       white-space: nowrap;
       overflow: hidden;
       text-overflow: ellipsis;
    }
</style>
