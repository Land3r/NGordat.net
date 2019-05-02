import { axiosInstance as axios } from 'boot/axios'
import { handleResponse, handleError } from './ServiceHelper'
import { API, getApiEndpoint } from 'data/backend'

const endpoints = {
  'DEFAULT': '/'
}

/**
 * SecurityService class.
 */
export default class SecurityService {
  doSendPublicKey (key) {
    console.log(key)
    const requestOptions = {
      method: 'post',
      url: getApiEndpoint(API.SECURITY + endpoints.DEFAULT),
      data: {
        key: key
      }
    }

    return axios(requestOptions)
      .then(function (response) {
        return handleResponse(response)
      })
      .catch(function (error) {
        return handleError(error)
      })
  }
}
