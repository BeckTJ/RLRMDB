import React from 'react';
import {View, Text, TextInput, StyleSheet, FlatList} from 'react-native';
import CarbonDrum from './CarbonDrum';
import FeedDrum from './FeedDrum';
import InputValue from './InputValue';

export default PreStart = data => {
  const material = data.material;

  return (
    <View style={styles.preStart}>
      <View>
        <View style={styles.drum}>
          <FeedDrum />
          <View>
            <CarbonDrum material={material} />
          </View>
        </View>
        <FlatList
          numColumns={2}
          style={styles.checks}
          data={material}
          renderItem={({item, index}) => (
            <View style={styles.checksView}>
              <Text style={styles.preStartText}>{item}</Text>
              <TextInput style={styles.preStartInput} />
            </View>
          )}
        />
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  preStart: {
    flexWrap: 'wrap',
    flexDirection: 'row',
    borderWidth: 1.5,
    borderColor: 'black',
    padding: 5,
    marginBottom: 5,
  },
  preStartText: {
    fontSize: 16,
    marginRight: 10,
  },
  preStartInput: {
    fontSize: 20,
    borderColor: 'black',
    borderBottomWidth: 1.5,
    width: 125,
    marginRight: 11,
    paddingBottom: -25,
  },
  drum: {
    flexWrap: 'wrap',
    flexDirection: 'row',
  },
  checks: {
    flexWrap: 'wrap',
    flexDirection: 'row',
  },
  checksView: {
    flexWrap: 'wrap',
    flexDirection: 'row',
    alignItems: 'baseline',
    marginRight: 10,
  },
});
