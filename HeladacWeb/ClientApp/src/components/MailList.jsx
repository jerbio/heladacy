import Constants from '../Constants'

import React, { Component, useState, useEffect } from "react";

import MailRow from './MailRow'

import MailApi from '../services/MailApi'
import CredentialApi from '../services/CredentialApi'


const apiObject = {
    mail: new MailApi(),
    credential: new CredentialApi()
}

const listConfigObject = {
    index: 0,
    pageSize: 20
}

const dataLoadObject = {
    status: Constants.loadStatus.notInitialized,
    data: {}
}

const MailList = (props) => {

    const [currentCount, setCurrentCount] = useState(0)
    const [api, setApi] = useState(apiObject)
    const [listConfig, setListConfig] = useState(listConfigObject)
    const [dataLoad, setDataLoad] = useState(dataLoadObject)

    useEffect(()=>{
        setDataLoad(
            {
            status: Constants.loadStatus.loading,
            data: Object.assign(dataLoad.data)
            })

        api.mail.getMails()
            .then((response)=>{
                setDataLoad({
                    status: Constants.loadStatus.finished,
                    data: response
                    })
            })
            .catch(()=>{
                setDataLoad({
                    status: Constants.loadStatus.error,
                    data: Object.assign(dataLoad.data)
                })
            })
    }, [])

    const generateCredential = () => {
        // debugger
        let credArgs = {
            url: 'netflix.com',
            domain: 'netflix.com'
        }
        api.credential.createCredential(credArgs).then((response) => {

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
    
        let status = null
        if (dataLoad.status === Constants.loadStatus.loading) {
        status = (<div>
            "Loading emails"
        </div>)
        } else if (dataLoad.status === Constants.loadStatus.error) {
        status = (<div>
            "Failed to load emails"
        </div>)
        }

        
        let mailRows = []

        if (dataLoad.status === Constants.loadStatus.finished) {
            const mails = dataLoad.data || []
            mailRows = mails.map((mail) => {
                return <MailRow key={'mail-row-'+mail.id} mail={mail} />
            })
            
        }

        return (
            <div>
                <div>{status}</div>
                <button onClick={generateCredential.bind(this)}>{"Create credential"}</button>
                <div>
                    {mailRows}
                </div>
            </div>
        );
    
}

export default MailList;