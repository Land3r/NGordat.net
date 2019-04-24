import axios from 'axios'

export const axiosInstance = axios

export default async ({ Vue }) => {
  Vue.prototype.$axios = axiosInstance
}
