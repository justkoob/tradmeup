# 09/23/2020 - Initial Design Thoughts
- Alpaca API x
- Console Application x
- Shared Library x
- Logging x
  - File x
  - Console x
- Scheduler x
  - Connection Handler x
- Configuration x
  - live mode x
  - live and paper keys x
  - strategies x
  - extended hours x
- Connection Handler - In Scheduler? x

# 09/28/2020 - Remaining tasks 
- Reusable Strategy Implementation
- Indicators
  - Simple Moving Average (SMA)
  - Rate of Change Percentage (ROCP)
- Scheduler
  - Indicator Data Subscription Updates
- Request Queueing System

# 09/29/2020 - Streaming data subscription thoughts
- Implement a dictionary <string, Security> x
- Security type would have the following: x
  - Symbol x
  - IStreamAggDelegate OnMinuteAggReceived x
  - IStreamAggDelegate OnSecondAggReceived
  - LastOrder
  - LastFilledOrder
  - Invested
  - TransactionDate
  - OrderHistroy

# 09/29/2020 - Remaining tasks
- Reusable Strategy Implementation - partially done
  - Potentially still need state data for tracking indicators specific to strategies
- Implement AlpacaStreamingClient_OnTradeUpdate
- Indicators
  - Simple Moving Average (SMA)
  - Rate of Change Percentage (ROCP)
- Scheduler
  - Indicator Data Subscription Updates
- Request Queueing System

# 09/30/2020 - Remaining tasks
- Reusable Strategy Implementation - partially done
  - Potentially still need state data for tracking indicators specific to strategies
- Add a project for unit tests (specificaly SubtractTradingDays())
- Implement AlpacaStreamingClient_OnTradeUpdate
- Indicators
  - Simple Moving Average (SMA)
  - Rate of Change Percentage (ROCP)
- Scheduler
  - Indicator Data Subscription Updates
- Request Queueing System