import Constants from '../Constants'

import React, { Component, useState, useEffect, useContext } from "react";

import MailApi from '../services/MailApi'
import MailPreview from './MailPreview'
import {ActiveMail} from "../pages/MailPage"
import CredentialApi from '../services/CredentialApi'



function getMailPreviewDoms(previews) {
    let previewDoms = [];
    if(Array.isArray(previews)) {
        if(previews.length > 0) {
            previewDoms = previews.map((preview) => {
                return (<MailPreview key={preview.id} preview = {preview}></MailPreview>)
            });

            return previewDoms;
        } else {
            return <div>"No mails"</div>;    
        }
    }
    if(!previews) {
        return <div>"Loading emails ..."</div>;
    }
    
}


export function MailPreviews(props) {
    const [mailApi] = useState(new MailApi());
    const [isInitialLoad, setInitialLoad] = useState(true);
    const [previewData, setPreviewData] = useState({data: null, config: Constants.pageParams});
    const {mailId, setMailId} = useContext(ActiveMail);

    if(isInitialLoad) {
        mailApi.getPreviews(previewData.config).then((response) => {
            let responsePreviewData = response.data;
            let index = response.index;
            let refreshedPreviewData = [...previewData.data||[]].concat(responsePreviewData);
            let config = previewData.config;
            config.index = index;
            previewData.config = config;
            setPreviewData({data: refreshedPreviewData, config});
        })
        setInitialLoad(false);
    }

    let previewDoms = getMailPreviewDoms(previewData.data);
    return (<div><div>{previewDoms}</div></div>);
};
