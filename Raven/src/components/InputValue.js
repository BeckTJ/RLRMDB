import React, {useState, useEffect} from 'react';
import {
  View,
  Text,
  StyleSheet,
  ScrollView,
  TextInput,
  FlatList,
  SectionList,
} from 'react-native';

export default InputValue = props => {
  const [number, onSubmitEditing] = useState(null);
  const [hourlyRead, setHourlyRead] = useState([InputValue]);

  function addHourlyRead() {
    setHourlyRead([...hourlyRead, InputValue]);
  }

  handlePress = () => {
    addHourlyRead();
  };
  return (
    <View style={styles.tableView}>
      <FlatList
        data={props.param}
        renderItem={({item, index}) => (
          <View style={styles.table}>
            <View style={styles.flatListView}>
              <Text style={styles.setPoint}>{item}</Text>
            </View>
            <ScrollView horizontal={true}>
              <View style={styles.table}>
                {hourlyRead.map(() => (
                  <TextInput
                    key={index}
                    style={styles.textInput}
                    keyboardType={'numeric'}
                    value={number}
                  />
                ))}
              </View>
            </ScrollView>
          </View>
        )}
      />

      <SmallButton
        onPress={handlePress}
        style={({pressed}) => [
          {backgroundColor: pressed ? '#4294b8' : '#3c545e'},
          styles.button,
        ]}
        title={'Submit'}
      />
    </View>
  );
};

const styles = StyleSheet.create({
  textInput: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    borderWidth: 1.5,
    borderColor: 'Black',
    height: 50,
    width: 75,
    fontSize: 24,
    textAlign: 'center',
    marginBottom: 5,
    marginRight: 5,
  },
  tableView: {
    flex: 1,
    marginBottom: 5,
    borderWidth: 1.5,
    borderColor: 'Black',
    padding: 5,
  },
  flatListView: {
    width: 550,
    height: 50,
    marginBottom: 5,
    marginRight: 5,
    borderWidth: 1.5,
    borderColor: 'Black',
  },
  table: {
    flexDirection: 'row',
    flexWrap: 'wrap',
  },
  setPoint: {
    padding: 10,
    fontSize: 18,
  },
  button: {
    width: 75,
    height: 50,
    borderWidth: 2,
    borderRadius: 10,
    borderColor: 'blue',
  },
});
