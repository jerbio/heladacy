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
