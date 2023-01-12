import React, { useState } from 'react';
import { Button } from 'react-bootstrap';
import './styles.css';
import classNames from 'classnames';

const SortToggleBtn = ({ title, onClick }: { title: string; onClick: () => void }) => {
  const [wasTouched, setWasTouched] = useState(false);
  const [isReverse, setIsReverse] = useState(true);

  function onClickHandler() {
    setIsReverse(!isReverse);
    if (!wasTouched) {
      setWasTouched(true);
    }
    onClick();
  }

  const classes = classNames('p-0 sort-toggle-btn d-inline-flex align-items-center', {
    'top-to-bottom': !isReverse,
    'bottom-to-top': isReverse,
  });
  return (
    <Button variant="none" className={classes} onClick={onClickHandler}>
      {title}
    </Button>
  );
};

export default SortToggleBtn;
