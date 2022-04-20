import React, { Component, useEffect, useContext } from "react";

import {ActiveMail} from "../pages/MailPage"
import Moment from 'react-moment';
import '../css/previewModule.scss';

function generateUserImage(preview) {

}

function generateMailPreviewText(preview) {

}

function MailPreview(props) {
    const {preview} = props
    const {mailId, setMailId} = useContext(ActiveMail);

    const imagePreview = generateUserImage(preview)
    const previewText = generateMailPreviewText(preview)
    const senderText = preview.senderName || preview.sender;
    let isSelectedClass = ''
    if (mailId === preview.id) {
        isSelectedClass = 'selected'
    }


    return <div className={isSelectedClass + ' preview-container'}>
            <div onClick={()=> setMailId(preview.id)}>
                <div className={'preview-top-row'}>
                    <div className={'preview-image'}>{imagePreview}</div>
                    <div className={'preview-toprow-text'}>
                        <div className={'preview-sender'}>{senderText}</div>
                        <div className={'preview-subject'}>{preview.subject}</div>
                    </div>
                <div className={'preview-time'}>
                    <Moment fromNow>{(preview.time)}</Moment></div>
                </div>
                <div className={'preview-text'}>{previewText}</div>
            </div>
        </div>;
}

export default MailPreview;