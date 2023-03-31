import React, {useState, useEffect} from 'react';
import {Text, View, StyleSheet, TextInput} from 'react-native';
import ajax from '../ProductionAjax';
import SmallButton from '../components/SmallButton';
import Dropdown from '../components/DropDown';

const ProductInfo = (props, {navigation, route}) => {
  const material = props.route.params.data;
  const distillationOption = props.route.params.choice;
  const [productLot, setProductLot] = useState([]);
  const [batch, setBatch] = useState();
  const [processOrder, setProcessOrder] = useState();
  const [selectedReceiver, setSelectedReceiver] = useState(undefined);
  const [selectedVendor, setSelectedVendor] = useState(undefined);

  let LotInfo = {};

  useEffect(() => {
    async function getNextLot() {
      setProductLot(await ajax.fetchProduct(material.materialNumber));
    }
    getNextLot();
  }, [material.materialNumber]);

  const updateLotInfo = () => {
    LotInfo.lotNumber = productLot.productLotNumber;
    LotInfo.materialNumber = material.materialNumber;
    LotInfo.processOrder = processOrder;
    LotInfo.batchNumber = batch;
    LotInfo.reciever = selectedReceiver;
    LotInfo.vendor = selectedVendor;
    ajax.postProduct(LotInfo);
    // ajax.fetchRawMaterial(selectedVendor);
  };

  const handleSubmit = () => {
    updateLotInfo();
    props.navigation.push(distillationOption, {
      Data: material,
      Lot: LotInfo,
    });
  };
  const handleExit = () => {
    props.navigation.goBack();
  };

  return (
    <View style={styles.container}>
      <View style={styles.header}>
        <Text style={styles.header}>
          {distillationOption}: {material.materialName}
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
            onChangeText={setProcessOrder}
            value={processOrder}
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
          <Dropdown
            label={'Select Reciever'}
            data={productLot.receivers}
            onSelect={setSelectedReceiver}
          />
        </View>
        <View style={styles.dropdown}>
          <Text style={styles.text}>Raw Material: </Text>
          <Dropdown
            label={'Select Vendor'}
            data={productLot.vendors}
            onSelect={setSelectedVendor}
          />
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
export default ProductInfo;
