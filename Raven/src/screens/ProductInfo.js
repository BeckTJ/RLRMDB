import React, {useState, useEffect} from 'react';
import {Text, View, StyleSheet, TextInput} from 'react-native';
import ajax from '../ProductionAjax';
import Dropdown from '../components/DropDown';
import SmallButton from '../components/SmallButton';

export default ProductInfo = (props, {navigation, route}) => {
  const material = props.route.params.data;
  const distillationOption = props.route.params.choice;
  const [productLot, setProductLot] = useState([]);
  const [order, setOrder] = useState();
  const [batch, setBatch] = useState();

  let LotInfo = {};

  useEffect(() => {
    async function getLotNumber() {
      return setProductLot(await ajax.fetchProduct(material.materialNumber));
    }
    getLotNumber();
  }, []);

  const updateLotInfo = async () => {
    LotInfo.lotNumber = productLot.productLotNumber;
    LotInfo.materialNumber = productLot.materialNumber;
    LotInfo.processOrder = order;
    LotInfo.batchNumber = batch;
    LotInfo.reciever = 'A-123';
    LotInfo.sampleNumber = null;
    await ajax.postProduct(LotInfo);
  };

  handleSubmit = () => {
    updateLotInfo();
    props.navigation.push(distillationOption, {
      Data: material,
      Lot: LotInfo,
    });
  };
  handleExit = () => {
    props.navigation.goBack();
  };

  return (
    <View style={styles.container}>
      <View style={styles.header}>
        <Text style={styles.header}>
          {distillationOption}: {productLot.materialNumber}
        </Text>
      </View>
      <View style={styles.display}>
        <View style={styles.rm}>
          <Text style={styles.text}>Product Lot Number: </Text>
          <Text style={styles.productLotNumber}>
            {productLot.productLotNumber}
          </Text>
        </View>
        <View style={styles.rm}>
          <Text style={styles.text}>Process Order: </Text>
          <TextInput
            style={styles.productInput}
            keyboardType={'numeric'}
            onChangeText={setOrder}
            value={order}
          />
        </View>
        <View style={styles.rm}>
          <Text style={styles.text}>Batch #: </Text>
          <TextInput
            style={styles.productInput}
            keyboardType={'numeric'}
            onChangeText={setBatch}
            value={batch}
          />
        </View>
        <View style={styles.dropdown}>
          <Text style={styles.text}>Reciever: </Text>
          <Dropdown label={'Select Item'} />
        </View>
        <View style={styles.dropdown}>
          <Text style={styles.text}>Raw Material: </Text>
          <Dropdown label={'Select Item'} />
        </View>
      </View>
      <View style={styles.button}>
        <SmallButton
          onPress={handleSubmit}
          style={({pressed}) => [
            {backgroundColor: pressed ? '#4294b8' : '#3c545e'},
            styles.submitButton,
          ]}
          title={'Submit'}
        />
        <SmallButton
          onPress={handleExit}
          style={({pressed}) => [
            {backgroundColor: pressed ? '#4294b8' : '#3c545e'},
            styles.button,
          ]}
          title={'Exit'}
        />
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    margin: 20,
  },
  header: {
    fontSize: 48,
    marginBottom: 50,
  },
  display: {
    borderColor: 'black',
    borderWidth: 1.5,
    justifyContent: 'center',
    padding: 20,
  },
  rm: {
    flexWrap: 'wrap',
    flexDirection: 'row',
    alignItems: 'baseline',
    textAlign: 'center',
    paddingBottom: 25,
  },
  text: {
    fontSize: 36,
  },
  productLotNumber: {
    borderBottomWidth: 1.5,
    borderColor: 'black',
    fontSize: 32,
    textAlign: 'center',
    width: 350,
  },
  dropdown: {
    flexWrap: 'wrap',
    flexDirection: 'row',
    alignItems: 'baseline',
    paddingTop: 25,
  },
  productInput: {
    fontSize: 24,
    borderColor: 'black',
    borderBottomWidth: 1.5,
    width: 350,
    marginRight: 30,
  },
  button: {
    flexWrap: 'wrap',
    flexDirection: 'row',
    marginTop: 100,
    justifyContent: 'space-evenly',
  },
});
