//React components
import React, { Component, useState, useContext } from 'react';

//App Components
import {ActiveMail} from "../pages/MailPage"

//Api and services
import Constants from '../Constants'


import MailApi from '../services/MailApi'

export function MailContent () {
    const {mailId, setMailId} = useContext(ActiveMail);
    const [mailApi] = useState(new MailApi());
    const [currentMailid, setCurrentMail] = useState(null);
    const [mailData, setMailData]  = useState(null);
    const [mailRequestStatus , setMailRequestStatus]  = useState(Constants.loadStatus.notInitialized);

    let lookupMailId = currentMailid;
    if (currentMailid !== mailId) {
        lookupMailId = mailId;
        let param = {id: lookupMailId};
        setMailRequestStatus(Constants.loadStatus.loading);
        setCurrentMail(lookupMailId);
        mailApi.getMail(param).then((mail) => {
            setMailData(mail);
            setMailRequestStatus(Constants.loadStatus.finished);
        }).catch((err) => {
            setMailRequestStatus(Constants.loadStatus.error);
        })
    }


    let retValue = null;
    if(mailData) {
        retValue = (<div>
            <div>{mailData.sender}
            </div>
            <div>{mailData.receiver.address}</div>
            <div>{mailData.subject}</div>
        </div>);
    }


    

    return (retValue);
}