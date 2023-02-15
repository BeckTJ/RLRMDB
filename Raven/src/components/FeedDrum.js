import React from 'react';
import {View, Text, TextInput, StyleSheet, FlatList} from 'react-native';

const FeedDrum = () => {
  let feedDrum, batch, startWeight, endWeight;

  return (
    <View style={styles.preStart}>
      <View>
        <View style={styles.drumWeight}>
          <Text style={styles.preStartText}>Feed Drum #:</Text>
          <TextInput style={styles.preStartInput} value={feedDrum} />
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
  preStart: {
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
