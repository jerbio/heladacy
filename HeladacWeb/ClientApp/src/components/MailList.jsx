import Constants from '../Constants'

import React, { Component } from "react";

import MailApi from '../services/MailApi'
import CredentialApi from '../services/CredentialApi'

export class MailList extends Component {
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

    componentDidMount() {
        this.setState({
          dataLoad: {
            status: Constants.loadStatus.loading,
            data: Object.assign(this.state.dataLoad.data)
          }
        })
        this.state.api.mail.getMails()
          .then((response) => {

            this.setState({
              dataLoad: {
                status: Constants.loadStatus.finished,
                data: response
              }
            })
          })
          .catch(() => {
            this.setState({
              dataLoad: {
                status: Constants.loadStatus.error,
                data: Object.assign(this.state.dataLoad.data)
              }
            })
          })
    }

    generateCredential() {
        debugger
        let credArgs = {
            url: 'netflix.com',
            domain: 'netflix.com'
        }
        this.state.api.credential.createCredential(credArgs).then((response) => {

            console.log(response)
            let retValue = response.json().then((credentialGenerated) => {
                return credentialGenerated
            }).catch((err) => {
                throw err
            });
            return retValue;
        }).catch((err) => {

            console.log(err)
            //let retValue = response.json();
            //return retValue;
        })
    }

    
    
    render () {
        let status = null
        if (this.state.dataLoad.status === Constants.loadStatus.loading) {
        status = (<div>
            "Loading emails"
        </div>)
        } else if (this.state.dataLoad.status === Constants.loadStatus.error) {
        status = (<div>
            "Failed to load emails"
        </div>)
        }

        
        let mailRows = []

        if (this.state.dataLoad.status === Constants.loadStatus.finished) {
            const mails = this.state.dataLoad.data || []
            mailRows = mails.map((mail) => {
                return <MailRow key = {'mail-row-'+mail.id} mail={mail} ></MailRow>
            })
            
        }

        return (
            <div>
                <div>{status}</div>
                <button onClick={this.generateCredential.bind(this)}>{"Create credential"}</button>
                <div>
                    {mailRows}
                </div>
            </div>
        );
    }
}

class MailRow extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        const {sender, subject, receiver} = this.props.mail
        let receiverLabelString = ''
        if (receiver) {
            receiverLabelString = receiver.name || receiver.address
        }

        return (
            <div>
                <span>
                    {sender}
                </span>
                <span>||</span>
                <span>
                    {subject}
                </span>
                <span>||</span>
                <span>
                    {receiverLabelString}
                </span>
            </div>
        )
    }
}