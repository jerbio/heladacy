import React, { Component, useEffect, useContext } from "react";

import {ActiveMail} from "../pages/MailPage"
import Moment from 'react-moment';
import styles from '../css/preview.module.css'; 

function generateUserImage(preview) {

}

function generateMailPreviewText(preview) {

}

function MailPreview(props) {
    const {preview} = props
    const {mailId, setMailId} = useContext(ActiveMail);

    const imagePreview = generateUserImage(preview)
    const previewText = generateMailPreviewText(preview)

    return <div className={'preview-container'}>
            <div onClick={()=> setMailId(preview.id)}>
                <div className={'preview-top-row'}>
                    <div className={'preview-image'}>{imagePreview}</div>
                    <div>
                        <div className={'preview-sender'}>{preview.sender}</div>
                        <div className={'preview-subject'}>{preview.subject}</div>
                    </div>
                    <div className={'preview-time'}><Moment unix>{(preview.time/1000)}</Moment></div>
                </div>
                <div className={'preview-text'}>{previewText}</div>
            </div>
        </div>;
}

export default MailPreview;