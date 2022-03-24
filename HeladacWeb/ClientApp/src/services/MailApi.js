import { Api } from './Api'
import axios from 'axios'

export default class MailApi extends Api {
    constructor() {
        super()
        let url = this.url+'api/mail/'
        this.url = url
    }

    async getMail(mailArgs) {
        let params = mailArgs
        let {id} = params
        let url = this.url.substr(0,this.url.length - 1);
        if (id) {
            url = new URL(url);
            Object.keys(params).forEach(key => url.searchParams.append(key, params[key]));
            let header = await this.getHeader();
            
            console.log(header)
            let response = fetch(url,{
                headers: header
            })
            return response.then((response) => {
                return response.json()
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
        let url = new URL(this.url + 'previews');
        let pageIndex = mailArgs.pageIndex ? mailArgs.pageIndex : 0;
        let pageSize = mailArgs.pageSize;
        let params = {pageIndex, pageSize};
        Object.keys(params).forEach(key => url.searchParams.append(key, params[key]));
        let header = await this.getHeader();
        
        console.log(header)
        let response = fetch(url,{
            headers: header
        })
        return response.then((response) => {
            return response.json()
        })
    }
}