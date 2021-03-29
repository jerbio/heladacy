import React from 'react'


const MailRow = (props) => {

    const {sender, subject, receiver} = props.mail
    let receiverLabelString = ''
    if (receiver) {
        receiverLabelString = receiver.name || receiver.address
    }

    return(
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

export default MailRow;