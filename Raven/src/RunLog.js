import React, {useState, useEffect} from 'react';
import {
  View,
  Text,
  StyleSheet,
  FlatList,
  ScrollView,
  TextInput,
} from 'react-native';
import InputValue from './components/InputValue';
import ajax from './components/ajax';
import SmallButton from './components/SmallButton';

export default RunLog = (props, {navigation, route}) => {
  const [read, setRead] = useState([]);
  const [input, setInput] = useState([]);
  const [number, onSubmitEditing] = useState(null);
  // const data = props.route.params.data;

  useEffect(() => {
    async function setDistillation() {
      setRead(await ajax.fetchRunLog(58913));
    }
    setDistillation();
  }, []);

  handlePress = () => {
    setInput(hourlyRead);
  };
  getInput = () => {};

  const hourlyRead = read.map((read, index) => (
    <TextInput style={styles.textInput} value={number} />
  ));

  return (
    <View style={styles.container}>
      <View>
        <Text style={styles.text}>Run Log: {props.name}</Text>
      </View>
      <View style={styles.tableView}>
        <FlatList
          data={read}
          renderItem={({item}) => (
            <View style={styles.flatListView}>
              <Text style={styles.setPoint}>{item.nomenclature}</Text>
              <Text style={styles.setPoint}>[{item.indicator}]</Text>
              <Text style={styles.setPoint}>
                ({item.setPoint} +\-{item.variance})
              </Text>
            </View>
          )}
        />
        <ScrollView horizontal={true} style={styles.scrollView}>
          <View style={styles.inputView}>{hourlyRead}</View>
        </ScrollView>
      </View>

      <SmallButton
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
  container: {
    margin: 2.5,
  },
  text: {
    fontSize: 42,
  },
  tableView: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    padding: 5,
    borderWidth: 1.5,
    borderColor: 'Black',
  },
  flatListView: {
    flexDirection: 'row',
    width: 550,
    height: 50,
    marginBottom: 5,
    borderWidth: 1.5,
    borderColor: 'Black',
  },
  setPoint: {
    padding: 10,
    fontSize: 18,
  },
  inputView: {
    marginRight: 195,
  },
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
  },
  button: {
    maxWidth: 75,
    maxHeight: 50,
    borderWidth: 2,
    borderRadius: 10,
    borderColor: 'blue',
  },
});
