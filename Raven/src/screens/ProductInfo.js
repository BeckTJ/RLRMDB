import React, {useState, useEffect} from 'react';
import {Text, View, StyleSheet, TextInput} from 'react-native';
import Dropdown from '../components/DropDown';
import SmallButton from '../components/SmallButton';

export default ProductInfo = (props, {navigation, route}) => {
  const material = props.route.params.data;
  const distillationOption = props.route.params.choice;
  const vender = ['lost', 'time'];
  let po, batch, lotNumber, receiver;

  handleSubmit = () => {
    navigation.push(distillationOption, {data: material});
  };
  handleExit = () => {};

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
          <TextInput
            style={styles.productInput}
            keyboardType={'default'}
            value={lotNumber}
          />
        </View>
        <View style={styles.rm}>
          <Text style={styles.text}>Process Order: </Text>
          <TextInput
            style={styles.productInput}
            keyboardType={'numeric'}
            value={po}
          />
        </View>
        <View style={styles.rm}>
          <Text style={styles.text}>Batch #: </Text>
          <TextInput
            style={styles.productInput}
            keyboardType={'numeric'}
            value={batch}
          />
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
    paddingBottom: 25,
  },
  text: {
    fontSize: 36,
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
