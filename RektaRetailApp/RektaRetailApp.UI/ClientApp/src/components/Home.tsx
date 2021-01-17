import React, { Component } from 'react';
import DashboardCard from './DashboardCard';
import {Paper} from "@material-ui/core";

export class Home extends Component {
  static displayName = Home.name;

  render() {
    return (
      <div>
        <Paper>
          <DashboardCard name="Testing Dash Card" supplyDate={new Date()} />
          <DashboardCard name="Testing Dash Card" supplyDate={new Date()} />
        </Paper>
      </div>
    );
  }
}
