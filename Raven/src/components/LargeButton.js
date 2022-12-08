import {useNavigation} from '@react-navigation/native';
import React, {useEffect, useState} from 'react';
import {Pressable, StyleSheet, Text} from 'react-native';

export default LargeButton = props => {
  const [path, setPath] = useState();
  const navigation = useNavigation();
  const option = props.currentOption;
  const currentPath = props.currentRoute;

  useEffect(() => {
    setPath(props);
  });

  handleHomeScreenPress = (selection, path) => {
    if (selection === 'Reports') {
      navigation.navigate(selection);
    } else {
      navigation.navigate('Material Selection', {data: path});
    }
  };

  handlePress = (selection, path) => {
    navigation.navigate(selection, {data: path});
  };

  return (
    <Pressable
      onPress={() =>
        option !== 'Material Selection'
          ? handleHomeScreenPress(props, path)
          : handlePress(currentPath, path)
      }
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
