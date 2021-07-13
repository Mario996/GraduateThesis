<template>
  <v-app
    class="background"
    dark>
    <v-app-bar
      app
      color="primary"
      dark>
      <div class="d-flex align-center"
           @click="goHome">
        <v-img
          alt="ElasticPMT"
          class="shrink"
          contain
          height="60"
          width="50"
          :src="require('./assets/elasticsearch-logo.png')"
          transition="scale-transition" />
        <v-toolbar-title class="toolbar__title">
          lasticPMT
        </v-toolbar-title>
      </div>
      <v-spacer />
      <v-btn
        dark
        text
        class="mr-2"
        @click="openReportsPage">
        Reports
      </v-btn>
      <v-btn
        dark
        text
        class="mr-2"
        @click="openPrioritiesPage">
        Priorities
      </v-btn>
      <v-btn
        dark
        text
        class="mr-2"
        @click="openStatusesPage">
        Statuses
      </v-btn>
      <v-btn
        dark
        text
        class="mr-2"
        @click="openUsersPage">
        Users
      </v-btn>
      <v-menu offset-y>
        <template v-slot:activator="{ on }">
          <v-btn
            dark
            text
            class="mr-2"
            v-on="on">
            Projects
          </v-btn>
        </template>
        <v-list>
          <v-list-item
            v-for="(item, index) in items"
            :key="index"
            @click="pickOption(item, 'project')">
            <v-list-item-title>{{ item }}</v-list-item-title>
          </v-list-item>
        </v-list>
      </v-menu>
      <v-menu offset-y>
        <template v-slot:activator="{ on }">
          <v-btn
            dark
            text
            class="mr-2"
            v-on="on">
            Requirements
          </v-btn>
        </template>
        <v-list>
          <v-list-item
            v-for="(item, index) in items"
            :key="index"
            @click="pickOption(item, 'requirement')">
            <v-list-item-title>{{ item }}</v-list-item-title>
          </v-list-item>
        </v-list>
      </v-menu>
      <v-menu offset-y>
        <template v-slot:activator="{ on }">
          <v-btn
            dark
            text
            class="mr-2"
            v-on="on">
            Tasks
          </v-btn>
        </template>
        <v-list>
          <v-list-item
            v-for="(item, index) in items"
            :key="index"
            @click="pickOption(item, 'task')">
            <v-list-item-title>{{ item }}</v-list-item-title>
          </v-list-item>
        </v-list>
      </v-menu>
    </v-app-bar>
    <v-content>
      <router-view />
    </v-content>
  </v-app>
</template>

<script>
import router from './router/index'

export default {
    name: 'App',
    data: () => ({
        items: [
            'Create',
            'List'
        ]
    }),
    methods: {
        pickOption (option, menu) {
            if (option === 'Create' && menu === 'requirement') {
                router.push('/requirement')
            } else if (option === 'List' && menu === 'requirement') {
                router.push('/list-requirements')
            } else if (option === 'Create' && menu === 'task') {
                router.push('/task')
            } else if (option === 'List' && menu === 'task') {
                router.push('/list-tasks')
            } else if (option === 'Create' && menu === 'project') {
                router.push('/project')
            } else {
                router.push('/list-projects')
            }
        },
        openUsersPage () {
            router.push('/users')
        },
        openStatusesPage () {
            router.push('/statuses')
        },
        openPrioritiesPage () {
            router.push('/priorities')
        },
        openReportsPage () {
            router.push('/reports')
        },
        goHome () {
            router.push('/home')
        }
    }
}
</script>

<style>
.background {
    background-image: url("./assets/background_1.jpg")!important;
    color:black!important;
  }
</style>
