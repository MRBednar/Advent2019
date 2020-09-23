import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { DotNetCore } from './components/DotNetCore';
import { Python } from './components/Python';
import { Ruby } from './components/Ruby';
import { Java } from './components/Java';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
            <Route path='/dotNet' component={DotNetCore} />
            <Route path='/python' component={Python} />
            <Route path='/ruby' component={Ruby} />
            <Route path='/java' component={Java} />
      </Layout>
    );
  }
}
