import Vue from 'vue'
import VueRouter from 'vue-router'

Vue.use(VueRouter)

const originalPush = VueRouter.prototype.push
VueRouter.prototype.push = function push (location) {
    return originalPush.call(this, location).catch(err => err)
}

const routes = [
    {
        path: '*',
        component: () => import(/* webpackChunkName: "home" */ '../views/Home.vue')
    },
    {
        path: '/list',
        component: () => import(/* webpackChunkName: "list" */ '../views/RequirementsList.vue')
    },
]

const router = new VueRouter({
    mode: 'history',
    routes
})

export default router
