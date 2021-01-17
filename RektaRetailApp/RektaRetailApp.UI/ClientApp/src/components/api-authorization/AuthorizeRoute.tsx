import React from "react";
import { Component } from "react";
import { Route, Redirect } from "react-router-dom";
import {
  ApplicationPaths,
  QueryParameterNames,
} from "./ApiAuthorizationConstants";
import authService from "./AuthorizeService";

interface IProps {
  component: any;
  path: any;
}

interface IState {
  ready: boolean;
  authenticated: boolean;
}

export default class AuthorizeRoute extends Component<IProps, IState> {
  _subscription: any;

  constructor(props: IProps) {
    super(props);

    this.state = {
      ready: false,
      authenticated: false,
    };
  }

  async componentDidMount() {
    this._subscription = authService.subscribe(() =>
      this.authenticationChanged()
    );
    await this.populateAuthenticationState();
  }

  componentWillUnmount() {
    authService.unsubscribe(this._subscription);
  }

  render() {
    const { ready, authenticated } = this.state;
    const redirectUrl = `${ApplicationPaths.Login}?${
      QueryParameterNames.ReturnUrl
    }=${encodeURI(window.location.href)}`;
    if (!ready) {
      return <div></div>;
    } else {
      const { component: Component, ...rest } = this.props;
      return (
        <Route
          {...rest}
          render={(props) => {
            if (authenticated) {
              return <Component {...props} />;
            } else {
              return <Redirect to={redirectUrl} />;
            }
          }}
        />
      );
    }
  }

  async populateAuthenticationState() {
    const authenticated = await authService.isAuthenticated();
    this.setState({ ready: true, authenticated });
  }

  async authenticationChanged() {
    this.setState({ ready: false, authenticated: false });
    await this.populateAuthenticationState();
  }
}