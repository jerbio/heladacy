import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';

import '../css/heladacPage.css'

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div class='heladac-page-container'>
        <div className='nav-menu-wrapper'><NavMenu /></div>
        <Container>
          {this.props.children}
        </Container>
      </div>
    );
  }
}
