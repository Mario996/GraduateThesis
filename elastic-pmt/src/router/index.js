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
        path: '/home',
        name: 'Home',
        component: () => import(/* webpackChunkName: "home" */ '../views/Home.vue')
    },
    {
        path: '/requirement',
        name: 'requirement',
        component: () => import(/* webpackChunkName: "about" */ '../views/AddRequirementView.vue'),
        props: true,
    },
    {
        path: '/list-requirements',
        name: 'list-requirements',
        component: () => import(/* webpackChunkName: "about" */ '../views/ListRequirementView.vue')
    },
    {
        path: '/task',
        name: 'task',
        component: () => import(/* webpackChunkName: "about" */ '../views/AddTaskView.vue'),
        props: true,
    },
    {
        path: '/list-tasks',
        name: 'list-tasks',
        component: () => import(/* webpackChunkName: "about" */ '../views/ListTaskView.vue')
    },
    {
        path: '/users',
        name: 'users',
        component: () => import(/* webpackChunkName: "about" */ '../views/UsersView.vue')
    },
    {
        path: '/statuses',
        name: 'statuses',
        component: () => import(/* webpackChunkName: "about" */ '../views/StatusesView.vue')
    },
    {
        path: '/project',
        name: 'project',
        component: () => import(/* webpackChunkName: "about" */ '../views/AddProjectView.vue'),
        props: true,
    },
    {
        path: '/list-projects',
        name: 'list-projects',
        component: () => import(/* webpackChunkName: "about" */ '../views/ListProjectView.vue')
    },
    {
        path: '/priorities',
        name: 'priorities',
        component: () => import(/* webpackChunkName: "about" */ '../views/PrioritiesView.vue')
    },
    {
        path: '/reports',
        name: 'reports',
        component: () => import(/* webpackChunkName: "about" */ '../views/ReportsView.vue')
    },
]

const router = new VueRouter({
    mode: 'history',
    routes
})

export default router
