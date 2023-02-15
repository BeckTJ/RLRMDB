import React from 'react';
import {Pressable, Text, StyleSheet} from 'react-native';

const LargeButton = props => {
  return (
    <Pressable
      onPress={props.onPress}
      style={({pressed}) => [
        {backgroundColor: pressed ? '#4294b8' : '#3c545e'},
        styles.button,
      ]}>
      <Text style={styles.text}>{props.name}</Text>
    </Pressable>
  );
};

const styles = StyleSheet.create({
  button: {
    alignItems: 'center',
    justifyContent: 'center',
    minWidth: 250,
    minHeight: 200,
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

export default LargeButton;
