import React, {useState, useEffect} from 'react';
import {
  View,
  Text,
  StyleSheet,
  FlatList,
  ScrollView,
  TextInput,
} from 'react-native';
import InputValue from '../components/InputValue';
import PreStart from '../components/PreStart';
import ajax from '../ajax';
import SmallButton from '../components/SmallButton';

export default RunLog = (props, {navigation, route}) => {
  const [read, setRead] = useState([]);
  const [preStart, setPreStart] = useState([]);
  let po, batch, lotNumber, receiver;

  const data = props.route.params.data;

  useEffect(() => {
    async function setDistillation() {
      setPreStart(await ajax.fetchPreStart(data.materialNumber));
      setRead(await ajax.fetchRunLog(data.materialNumber));
    }
    setDistillation();
  }, []);

  return (
    <View style={styles.container}>
      <View>
        <View style={styles.runLog}>
          <Text style={styles.text}>Run Log: {data.materialName}</Text>
          <View style={styles.header}>
            <Text style={styles.setPoint}>Lot Number:</Text>
            <TextInput
              style={styles.preStartInput}
              keyboardType={'default'}
              value={lotNumber}
            />
          </View>
        </View>
        <View style={styles.header}>
          <Text style={styles.setPoint}>Receiver:</Text>
          <TextInput
            style={styles.preStartInput}
            keyboardType={'default'}
            value={receiver}
          />
          <Text style={styles.setPoint}>Process Order:</Text>
          <TextInput
            style={styles.preStartInput}
            keyboardType={'numeric'}
            value={po}
          />
          <Text style={styles.setPoint}>Batch #:</Text>
          <TextInput
            style={styles.preStartInput}
            keyboardType={'numeric'}
            value={batch}
          />
        </View>
        <PreStart style={styles.checks} material={preStart} />
      </View>
      <InputValue param={read} />
    </View>
  );
};
const styles = StyleSheet.create({
  container: {
    flex: 1,
    margin: 5,
  },
  runLog: {
    flexWrap: 'wrap',
    flexDirection: 'row',
    alignItems: 'baseline',
    justifyContent: 'space-between',
  },
  checks: {
    flexWrap: 'wrap',
    flexDirection: 'row',
  },
  text: {
    fontSize: 42,
  },
  header: {
    flexWrap: 'wrap',
    flexDirection: 'row',
    justifyContent: 'center',
    alignItems: 'baseline',
    marginTop: -10,
    marginBottom: 5,
  },
  preStartInput: {
    fontSize: 18,
    borderColor: 'black',
    borderBottomWidth: 1.5,
    width: 150,
    marginRight: 30,
  },
  setPoint: {
    fontSize: 18,
    marginRight: 10,
  },
});
