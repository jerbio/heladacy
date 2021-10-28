//React components
import React, { Component } from 'react';

//App Components
import { MailPreview } from '../components/MailPreview'
import { MailContent } from '../components/MailContent'

//Api and services
import Constants from '../Constants'


import MailApi from '../services/MailApi'

export class MailPage extends Component {
    constructor(props) {
        super(props);
        this.state = {
            currentCount: 0,
            api: {
                mail: new MailApi()
            },
            pageConfig: {
                index: 0,
                pageSize: 20
            },
            dataLoad: {
                status: Constants.loadStatus.notInitialized,
                data: {}
            }
        };
    }

    render() {

        return (
          <div>
            <h1>Mail</h1>
            <div>
                <MailPreview></MailPreview>
            </div>
            <div>
                <MailContent></MailContent>
            </div>
            <h1>Heladac Mail is here</h1>
          </div>
        );
    }
}