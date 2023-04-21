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
import LotInformation from '../components/LotInformation';

const RunLog = (props, {navigation, route}) => {
  const [read, setRead] = useState([]);
  const [preStart, setPreStart] = useState([]);
  const [rawMaterial, setRawMaterial] = useState([]);

  const material = props.route.params.Data;
  const productLot = props.route.params.Lot;

  useEffect(() => {
    async function setDistillation() {
      setPreStart(await ajax.fetchPreStart(material.materialNumber));
      setRead(await ajax.fetchRunLog(material.materialNumber));
      setRawMaterial(
        await ajax.fetchRawMaterial(material.materialNumber, productLot.vendor),
      );
    }
    setDistillation();
  }, [material.materialNumber, productLot.vendor]);

  return (
    <View style={styles.container}>
      <View>
        <View style={styles.info}>
          <LotInformation param={productLot} material={material} />
        </View>
        <PreStart material={preStart} param={rawMaterial} />
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
  info: {
    flexWrap: 'wrap',
    flexDirection: 'row',
  },
  text: {
    fontSize: 42,
  },
});
export default RunLog;
