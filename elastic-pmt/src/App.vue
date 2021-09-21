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
          lastic Requirement Matcher
        </v-toolbar-title>
      </div>
      <v-btn class="ml-3"
             color="red lighten-1"
             dark
             @click="createIndex()">
        Populate index
      </v-btn>
      <v-spacer />
      <v-btn
        dark
        text
        class="mr-2"
        @click="openRequirementsList">
        Requirements list
      </v-btn>
    </v-app-bar>
    <v-content>
      <router-view />
    </v-content>
  </v-app>
</template>

<script>
import router from './router/index'
import { indexService } from './services/index-service'

export default {
    name: 'App',
    methods: {
        openRequirementsList () {
            router.push('/list')
        },
        goHome () {
            router.push('/home')
        },
        createIndex () {
            indexService.create().then((response) => {
                console.log(response)
            })
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
