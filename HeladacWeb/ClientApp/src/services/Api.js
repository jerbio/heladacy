import authService from '../authorization/AuthorizeService'

export class Api {
    constructor() {
        this.url = origin+'/'
    }

    async getHeader() {
        const token = await authService.getAccessToken();
        let retValue = !token ? {} : { 'Authorization': `Bearer ${token}` }
        return retValue
    }
}

