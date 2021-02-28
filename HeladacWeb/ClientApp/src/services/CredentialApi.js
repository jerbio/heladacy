import { Api } from './Api'

export default class CredentialApi extends Api {

    constructor() {
        super();
        let url = this.url + 'api/credential/'
        this.url = url
    }

    async createCredential(credentialArgs) {
        let pageUrl = window.location.href
        let domain = window.location.host
        let url = this.url;
        if (credentialArgs != null) {
            pageUrl = credentialArgs.url
            domain = credentialArgs.domain
        }

        let postData = {
            domain,
            fullUri: pageUrl
        }

        let header = await this.getHeader()
        header['Content-Type'] = 'application/json'
        debugger
        return fetch(url,
            {
                method: 'POST',
                headers: header,
                //headers: {
                //    'Content-Type': 'application/json'
                //},
                body: JSON.stringify(postData) 
            }
        )
    }
}