import Constants from '../Constants'

import React, { Component } from "react";

import MailApi from '../services/MailApi'
import CredentialApi from '../services/CredentialApi'

export class MailPreview extends Component {
    constructor(props) {
        super(props);
        this.state = {
            currentCount: 0,
            api: {
                mail: new MailApi(),
                credential: new CredentialApi()
            },
            listConfig: {
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
            <div>Mail Preview</div>
          </div>
        );
    }
}