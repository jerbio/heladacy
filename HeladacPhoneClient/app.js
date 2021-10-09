'use strict';

console.log('Hello world');

const accountSid = process.env.TWILIO_ACCOUNT_SID;
const authToken = process.env.TWILIO_AUTH_TOKEN;
const client = require('twilio')(accountSid, authToken);

client.availablePhoneNumbers('US')
    .local
    .list({ areaCode: 510, limit: 20 })
    .then(local => local.forEach(l => console.log(l.friendlyName)));