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
import ajax from '../ProductionAjax';
import SmallButton from '../components/SmallButton';

const RunLog = (props, {navigation, route}) => {
  const [read, setRead] = useState([]);
  const [preStart, setPreStart] = useState([]);

  const material = props.route.params.Data;
  const productLot = props.route.params.Lot;

  useEffect(() => {
    async function setDistillation() {
      setPreStart(await ajax.fetchPreStart(material.materialNumber));
      setRead(await ajax.fetchRunLog(material.materialNumber));
    }
    setDistillation();
  }, [material.materialNumber]);

  return (
    <View style={styles.container}>
      <View>
        <View style={styles.runLog}>
          <Text style={styles.text}>Run Log: {material.materialName}</Text>
          <View style={styles.header}>
            <Text style={styles.setPoint}>Lot Number:</Text>
            <Text style={styles.product}>{productLot.lotNumber}</Text>
          </View>
        </View>
        <View style={styles.header}>
          <View style={styles.productView}>
            <Text style={styles.setPoint}>Receiver:</Text>
            <Text style={styles.product}>{productLot.reciever}</Text>
          </View>
          <View style={styles.productView}>
            <Text style={styles.setPoint}>Process Order:</Text>
            <Text style={styles.product}>{productLot.processOrder}</Text>
          </View>
          <View style={styles.productView}>
            <Text style={styles.setPoint}>Batch #:</Text>
            <Text style={styles.product}>{productLot.batchNumber}</Text>
          </View>
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
    marginBottom: 5,
  },
  product: {
    marginRight: 25,
    borderBottomWidth: 1.5,
    borderColor: 'black',
    fontSize: 20,
    textAlign: 'center',
    width: 125,
  },
  productView: {
    flexWrap: 'wrap',
    flexDirection: 'row',
    alignItems: 'baseline',
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
export default RunLog;
