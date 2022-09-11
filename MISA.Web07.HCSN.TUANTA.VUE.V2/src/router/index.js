import { createRouter, createWebHistory } from 'vue-router'
import Dashboard from '../views/TheDashboard.vue'
import table from '../components/base/BaseTable/TheTable.vue'
const routes = [
  {
    path: '/',
    name: 'dashboard',
    component:Dashboard,
  },
  {
    path : '/table',
    name : 'table',
    component : table
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
