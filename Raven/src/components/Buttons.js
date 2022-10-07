import React from 'react';
import {Pressable, StyleSheet, Text} from 'react-native';
import {useNavigation} from '@react-navigation/native';

const Buttons = props => {
  const navigation = useNavigation();
  return (
    <Pressable
      onPress={() => navigation.navigate(props)}
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
    borderRadius: 10,
    borderColor: 'blue',
    borderWidth: 2,
    alignSelf: 'flex-start',
    marginBottom: '15%',
    minWidth: 225,
    minHeight: 200,
  },
  text: {
    fontSize: 24,
    fontWeight: 'bold',
    color: 'white',
  },
});

export default Buttons;
