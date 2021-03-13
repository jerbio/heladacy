//React components
import React, { Component } from 'react';

//App Components
import {MailList} from '../components/MailList'

//Api and services
import Constants from '../Constants'


import MailApi from '../services/MailApi'

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
        }
    };
  }

  incrementCounter() {
    this.setState({
      currentCount: this.state.currentCount + 1
    });
    }

    componentDidMount() {
        debugger
        const currentOrigin = window.location.origin
        window.addEventListener("message", (event) => {
            
            if (event.origin !== currentOrigin) {
                console.log(event.origin)
                console.log(event.data)
                debugger
                this.handleChromeWebMessage(event)

                return;
            }
        }, false);
    }

    handleChromeWebMessage(postMessageData) {
        if (postMessageData.data && postMessageData.data.heladacChromeStore) {
            let heladacChromeStore = postMessageData.data.heladacChromeStore
            let isUserFound = false
            if (heladacChromeStore && heladacChromeStore.oidcStore) {
                let heladacUserStore = heladacChromeStore
                let oidcKeys = Object.keys(heladacUserStore.oidcStore)
                let callBackId = heladacChromeStore.callBackId
                oidcKeys.forEach((oidcKey) => {
                    isUserFound = true
                    let oidcData = heladacUserStore.oidcStore[oidcKey]
                    localStorage.setItem(oidcKey, oidcData);
                })
                this.confirmUserStoreUpdate(postMessageData.origin, isUserFound, callBackId)
            }
        }
    }

    confirmUserStoreUpdate(origin, isUserFound, callBackId) {
        debugger
        let serverPath = origin +'/popup.html'
        let iFrameElement = document.createElement("iframe");
        iFrameElement.setAttribute('src', serverPath);
        iFrameElement.style.display = 'none'
        document.body.appendChild(iFrameElement);
        let windowForPost = iFrameElement.contentWindow
        setTimeout(() => {
            let data = {
                heladacDataTransfer: {
                    oidcStoreTransfer: {
                        isUserFound: isUserFound,
                        callBackId
                    }
                }
            }
            windowForPost.postMessage(data, '*');
            
        }
        , 3000)
    }

  render() {

    return (
      <div>
        <h1>Mail</h1>
        <MailList></MailList>
        <h1>Heladac Mail is here</h1>
      </div>
    );
  }
}
