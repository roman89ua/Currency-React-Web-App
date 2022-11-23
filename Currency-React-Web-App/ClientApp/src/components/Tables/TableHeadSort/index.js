import React, {useEffect, useState} from 'react';
import SortToggleBtn from "../../Buttons/SortToggleBtn";

const TableHeadSort = ({setCurrency, title, fieldKey}) => {
  /*
  Sort order can be used for now only "ascending" or "descending"
  */
  const [sortOrder, setSortOrder] = useState("ascending");

  /*
    objKey can be only
        objKeyEnum {
            Currency,
            ExchangeDate,
            Rate,
            Text,
        }
     */
  const onHandleClick = async (objKey, order) => {
    const response = await fetch(`currencycurrentdate/sortcurrencydata/${objKey}/${order}`);
    const data = await response.json();
    console.log(data);
    if (!!data) {
      setCurrency(data);
    }
    (sortOrder === "descending") ? setSortOrder("ascending") : setSortOrder("descending");
  }

  return (
    <th scope="col"><SortToggleBtn title={title} onClick={() => onHandleClick(fieldKey, sortOrder)}/></th>
  );
}

export default TableHeadSort;
