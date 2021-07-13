<template>
  <v-container
    fluid
    class="fill-height">
    <v-row
      align="center"
      justify="center">
      <v-col cols="12"
             align="center"
             class="mb-12">
        <h1>
          Welcome to Elastic Project Management Tool
        </h1>
      </v-col>
      <v-col cols="6"
             align="center">
        <v-card
          height="200"
          width="250"
          flat
          class="v-card-border"
          @click="listRequirements">
          <p class="big-number">
            {{ numberOfRequirements }}
          </p>
          <p>
            REQUIREMENT(S) CREATED
          </p>
        </v-card>
      </v-col>
      <v-col cols="6"
             align="center">
        <v-card
          height="200"
          width="250"
          flat
          class="v-card-border"
          @click="listTasks">
          <p class="big-number">
            {{ numberOfTasks }}
          </p>
          <p>
            TASK(S) CREATED
          </p>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { requirementsService } from '../services/requirements-service'
import { tasksService } from '../services/tasks-service'
import router from '../router/index'

export default {
    name: 'Home',
    data: () => ({
        numberOfRequirements: 0,
        numberOfTasks: 0,
    }),
    created () {
        requirementsService.getAllRequirements()
            .then((response) => {
                this.numberOfRequirements = response.length
            })
        tasksService.getAllTasks()
            .then((response) => {
                this.numberOfTasks = response.length
            })
    },
    methods: {
        listRequirements () {
            router.push('/list-requirements')
        },
        listTasks () {
            router.push('/list-tasks')
        }
    }
}
</script>

<style>
.v-card-border{
  border: 2px solid black!important;
  background:inherit!important;
}
.big-number{
  font-size: 8vh;
}
</style>
