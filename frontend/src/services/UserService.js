import { axiosInstance as axios } from 'boot/axios'
import { handleResponse, handleError } from './ServiceHelper'
import { API, getApiEndpoint } from 'data/backend'

const endpoints = {
  'DEFAULT': '/',
  'FORGOTPASSWORD': '/forgot_password'
}

/**
 * UserService class.
 */
export default class UserService {
  /**
   * Ask server for password lost using user's email.
   * @param {string} email The user's email.
   */
  forgotPasswordEmail (email) {
    const requestOptions = {
      method: 'post',
      url: getApiEndpoint(API.USER + endpoints.FORGOTPASSWORD),
      data: {
        email: email
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

  /**
   * Ask server for password lost using user's username.
   * @param {string} username The user's username.
   */
  forgotPasswordUsername (username) {
    const requestOptions = {
      method: 'post',
      url: getApiEndpoint(API.USER + endpoints.FORGOTPASSWORD),
      data: {
        username: username
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

  create (user) {
    const requestOptions = {
      method: 'post',
      url: getApiEndpoint(API.USER + endpoints.DEFAULT),
      data: {
        username: user.username,
        firstname: user.firstname,
        lastname: user.lastname,
        email: user.email,
        password: user.password
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
