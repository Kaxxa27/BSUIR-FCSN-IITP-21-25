import React, { Component } from 'react';

class IpComponent extends Component {
  constructor(props) {
    super(props);
    this.state = {
      ip: '',
      error: null,
    };
  }

  componentDidMount() {
    this.getIp();
  }

  getIp = async () => {
    try {
      const response = await fetch('https://api.ipify.org?format=json');

      if (response.ok) {
        const data = await response.json();
        this.setState({ ip: data.ip });
      } else {
        throw new Error(`Error: ${response.status}`);
      }
    } catch (error) {
      this.setState({ error: `Failed to fetch IP: ${error.message}` });
    }
  };

  render() {
    const { ip, error } = this.state;

    return (
      <div>
        <h2>IP Address:</h2>
        {error ? <p>{error}</p> : <p>{ip}</p>}
      </div>
    );
  }
}

export default IpComponent;
