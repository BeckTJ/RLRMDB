import React, {useState, useEffect} from 'react';
import {View, StyleSheet, FlatList} from 'react-native';
import PropTypes from 'prop-types';
import Buttons from './Buttons';

class Material extends React.Component {
  static propTypes = {
    material: PropTypes.array.isRequired,
  };
  render() {
    return (
      <View>
        <FlatList
          data={this.props.material}
          renderItem={({item}) => <Text>item.title</Text>}></FlatList>
      </View>
    );
  }
}
export default Material;
