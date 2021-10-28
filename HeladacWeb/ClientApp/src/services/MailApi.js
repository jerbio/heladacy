//React components
import React, { Component } from 'react';
import { Api } from './Api'
import axios from 'axios'

export default class MailApi extends Api {
    constructor() {
        super()
        let url = this.url+'api/mail/'
        this.url = url
    }

    getMail(mailArgs) {
        let params = mailArgs
        let {id} = params
        let url = this.url
        if (id) {
            return axios.get(url, {
                params
              }).then((response) => {
                  return response.data
              })
        } else {
            return Promise.reject(() => {
                return {error: 'id missing'}
            })
        }
    }

    async getMails(mailArgs) {
        let url = this.url + 'usermails'
        let header = await this.getHeader()
        console.log(header)
        let response = fetch(url,{
            headers: header
        })
        return response.then((response) => {
            return response.json()
        })
    }

    async getPreviews(mailArgs) {
        let url = this.url + 'previews'
        let header = await this.getHeader()
        console.log(header)
        let response = fetch(url,{
            headers: header
        })
        return response.then((response) => {
            return response.json()
        })
    }
}