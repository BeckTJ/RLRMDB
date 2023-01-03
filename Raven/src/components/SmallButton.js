import React, {useState, useEffect} from 'react';
import {Text, StyleSheet, Pressable} from 'react-native';

export default SmallButton = props => {
  return (
    <Pressable
      onPress={props.onPress}
      style={({pressed}) => [
        {backgroundColor: pressed ? '#4294b8' : '#3c545e'},
        styles.button,
      ]}>
      <Text style={styles.text}>{props.title}</Text>
    </Pressable>
  );
};
const styles = StyleSheet.create({
  button: {
    alignItems: 'center',
    justifyContent: 'center',
    minWidth: 150,
    minHeight: 125,
    borderWidth: 2,
    borderRadius: 10,
    borderColor: 'blue',
  },
  text: {
    fontSize: 32,
    fontWeight: 'bold',
    color: 'white',
  },
});
