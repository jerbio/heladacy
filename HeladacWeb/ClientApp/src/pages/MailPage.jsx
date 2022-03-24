﻿//React components
import React, { Component, createContext  } from 'react';

//App Components
import { MailPreviews } from '../components/MailPreviews'
import { MailContent } from '../components/MailContent'

//Api and services
import Constants from '../Constants'


import MailApi from '../services/MailApi'

export const ActiveMail = createContext({
    mailId: null,
    setMailId: () => {},
});

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
            },
            activeMail: {
                mailId: null,
                setMailId: (val) => {
                    this.setState({
                        activeMail: 
                        {...this.state.activeMail,
                            mailId: val
                        }
                    })

                },
            }

        };
    }
    
    getMailContent() {
        if(this.state.activeMail.mailId) {
            return (<MailContent></MailContent>);
        }

        return null
    }

    render() {
        let mailContent = this.getMailContent();
        return (
        <ActiveMail.Provider value={this.state.activeMail}>
          <div>
            <h1>Mail</h1>
            <div>
                <MailPreviews></MailPreviews>
            </div>
            <div> {mailContent} </div>
          </div>
        </ActiveMail.Provider>
        );
    }
}