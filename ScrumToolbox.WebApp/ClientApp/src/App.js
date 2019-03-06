import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/layouts/Layout';
import { Home } from './components/pages/Home';

export default class App extends Component {
  displayName = App.name

  render() {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
      </Layout>
    );
  }
}
