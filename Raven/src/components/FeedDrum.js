import React, {useState, useEffect} from 'react';
import {View, Text, TextInput, StyleSheet, FlatList} from 'react-native';
import Dropdown from './DropDown';
import ajax from '../ProductionAjax';

const FeedDrum = props => {
  const [selection, setSelection] = useState();
  const rawMaterial = props.param;
  let feedDrum, batch, startWeight, endWeight;

  const handleSelection = async () => {
    setSelection(await ajax.fetchRawMaterialInfo(rawMaterial));
  };

  return (
    <View style={styles.feedDrum}>
      <View>
        <View style={styles.drumWeight}>
          <Text style={styles.preStartText}>Raw Material:</Text>
          <Dropdown
            style={styles.feedDrumDropdown}
            label={'Select Raw Material'}
            data={rawMaterial}
            onSelect={handleSelection}
          />
        </View>
        <View style={styles.drumWeight}>
          <Text style={styles.preStartText}>Batch #:</Text>
          <TextInput style={styles.preStartInput} value={batch} />
        </View>
      </View>
      <View>
        <View style={styles.drumWeight}>
          <Text style={styles.drumWeightText}>Start Weight:</Text>
          <TextInput style={styles.smallInput} value={startWeight} />
          <Text style={styles.drumWeightText}>kg </Text>
        </View>
        <View style={styles.drumWeight}>
          <Text style={styles.drumWeightText}>End Weight:</Text>
          <TextInput style={styles.smallInput} value={endWeight} />
          <Text style={styles.drumWeightText}>kg </Text>
        </View>
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  feedDrum: {
    flexWrap: 'wrap',
    flexDirection: 'row',
  },
  preStartText: {
    fontSize: 20,
    marginRight: 10,
  },
  preStartInput: {
    fontSize: 20,
    borderColor: 'black',
    borderBottomWidth: 1.5,
    width: 125,
    marginRight: 11,
    paddingBottom: 0,
  },
  feedDrumDropdown: {
    width: 250,
    borderStyle: 'solid',
    borderColor: 'black',
    borderWidth: 1.5,
  },
  smallInput: {
    fontSize: 20,
    borderColor: 'black',
    borderBottomWidth: 1.5,
    width: 50,
    marginRight: 10,
    paddingBottom: -5,
  },
  drumWeight: {
    flexWrap: 'wrap',
    flexDirection: 'row',
    alignItems: 'baseline',
  },
  drumWeightText: {
    fontSize: 14,
    marginRight: 5,
  },
});
export default FeedDrum;
