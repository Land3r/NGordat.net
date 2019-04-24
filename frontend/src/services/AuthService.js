import { axiosInstance as axios } from 'boot/axios'
import { handleResponse, handleError } from './ServiceHelper'
import { API, getApiEndpoint } from 'data/backend'

/**
 * AuthService class.
 */
export default class AuthService {
  /**
   * Logs the user.
   * @param {string} username The user's username.
   * @param {string} password The user's password.
   */
  doLogin (username, password) {
    const requestOptions = {
      method: 'post',
      url: getApiEndpoint(API.AUTH),
      data: {
        username: username,
        password: password
      }
    }
    return axios(requestOptions)
      .then(function (response) {
        return handleResponse(response)
      })
      .catch(function (error) {
        handleError(error)
      })
  }
}
