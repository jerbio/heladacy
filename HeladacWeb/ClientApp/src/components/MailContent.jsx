//React components
import React, { Component } from 'react';

//App Components
import { MailPreview } from '../components/MailPreview'

//Api and services
import Constants from '../Constants'


import MailApi from '../services/MailApi'

export class MailContent extends Component {

    render() {
        return (
            <div>
              <div>Mail content</div>
            </div>
          );
      }
}