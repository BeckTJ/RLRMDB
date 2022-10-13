import React, {useState, useEffect} from 'react';
import Buttons from '../Buttons';
import {View, Text, Stylesheet} from 'react-native';
import ajax from '../ajax';
import Material from '../Material';

class MaterialChoice extends React.Component {
  state = {
    material: [],
  };
  async componentDidMount() {
    const material = await ajax.fetchMaterial();
    this.setState({material});
  }
  render() {
    return (
      <View>
        <Material material={this.state.material} />
      </View>
    );
  }
}
export default MaterialChoice;
