import React from 'react';
import {View, Text, StyleSheet} from 'react-native';

const LotInformation = props => {
  const productLot = props.param;
  const material = props.material;

  return (
    <View style={styles.container}>
      <Text style={styles.header}>Run Log: {material.materialName}</Text>
      <View style={styles.sets}>
        <Text style={styles.text}>Lot Number:</Text>
        <Text style={styles.values}>{productLot.lotNumber}</Text>
      </View>
      <View style={styles.sets}>
        <Text style={styles.text}>Run:</Text>
        <Text style={styles.values}>{productLot.reciever}</Text>
      </View>
      <View style={styles.sets}>
        <Text style={styles.text}>Process Order:</Text>
        <Text style={styles.values}>{productLot.processOrder}</Text>
      </View>
      <View style={styles.sets}>
        <Text style={styles.text}>Batch #:</Text>
        <Text style={styles.values}>{productLot.batchNumber}</Text>
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flexWrap: 'wrap',
    flexDirection: 'row',
    margin: 5,
  },
  header: {
    fontSize: 40,
    marginRight: 75,
  },
  text: {
    fontSize: 20,
    marginRight: 2,
  },
  sets: {
    flexWrap: 'wrap',
    flexDirection: 'row',
    alignItems: 'baseline',
    margin: 5,
    marginTop: 15,
  },
  values: {
    borderBottomWidth: 1.5,
    width: 150,
    textAlign: 'center',
    fontSize: 20,
  },
});
export default LotInformation;
