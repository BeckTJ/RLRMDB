import React, {useState, useEffect} from 'react';
import {View, Text, StyleSheet, ScrollView, TextInput} from 'react-native';

export default InputValue = props => {
  const [userInput, onSubmitEditing] = useState(null);
  return (
    <View>
      <ScrollView horizontal={true} style={styles.scrollView}>
        <TextInput style={styles.inputValue} value={userInput} />
      </ScrollView>
    </View>
  );
};
const styles = StyleSheet.create({
  textInput: {
    borderWidth: 1.5,
    borderColor: 'Black',
    height: 50,
    width: 75,
    fontSize: 24,
    textAlign: 'center',
    marginBottom: 5,
  },
  scrollView: {
    // alignContent: 'flex-start',
    // marginRight: 195,
  },
});
